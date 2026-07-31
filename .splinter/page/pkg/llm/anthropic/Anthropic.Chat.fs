// §head page/pkg/llm/anthropic.go:83-147 Anthropic.Chat
// §sig func (a *Anthropic) Chat(ctx context.Context, req ChatRequest) (*ChatResponse, error)
	model := req.Model
	if model == "" {
		model = a.Model
	}
	if len(req.Messages) == 0 {
		return nil, fmt.Errorf("anthropic: chat request has no messages")
	}
	if model == "" {
		return nil, fmt.Errorf("anthropic: chat request model is empty")
	}
	maxTokens := req.MaxTokens
	if maxTokens == 0 {
		maxTokens = 4096
	}
	msgs := make([]anthropicMsg, len(req.Messages))
	for i, m := range req.Messages {
		msgs[i] = toAnthropicMsg(m)
	}
	body, err := json.Marshal(anthropicReq{
		Model:     model,
		MaxTokens: maxTokens,
		Messages:  msgs,
		Tools:     toAnthropicTools(req.Tools),
	})
	if err != nil {
		return nil, err
	}
	hr, err := http.NewRequestWithContext(ctx, http.MethodPost, a.BaseURL+"/v1/messages", bytes.NewReader(body))
	if err != nil {
		return nil, err
	}
	hr.Header.Set("x-api-key", a.APIKey)
	hr.Header.Set("anthropic-version", "2023-06-01")
	hr.Header.Set("content-type", "application/json")
	resp, err := a.HTTP.Do(hr)
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()
	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("anthropic: HTTP %d", resp.StatusCode)
	}
	var ar anthropicResp
	if err := json.NewDecoder(resp.Body).Decode(&ar); err != nil {
		return nil, err
	}
	out := &ChatResponse{
		Message: Message{Role: "assistant"},
		Usage:   Usage{InputTokens: ar.Usage.InputTokens, OutputTokens: ar.Usage.OutputTokens},
	}
	for _, b := range ar.Content {
		if b.Type == "text" {
			out.Message.Content += b.Text
		}
		if b.Type == "tool_use" {
			out.Message.ToolUse = &ToolCall{
				ID:    b.ID,
				Name:  b.Name,
				Input: b.Input,
			}
		}
	}
	return out, nil
// §foot page/pkg/llm/anthropic.go Anthropic.Chat