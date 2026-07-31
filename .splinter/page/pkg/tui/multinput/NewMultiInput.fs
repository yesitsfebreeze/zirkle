// §head page/pkg/tui/multinput.go:32-42 NewMultiInput
// §sig func NewMultiInput(width int) MultiInput
	ta := textarea.New()
	ta.ShowLineNumbers = false
	ta.CharLimit = 0
	ta.MaxHeight = 0 // no cap; parent controls visible height
	ta.SetWidth(width)
	ta.SetHeight(1)
	ta.Focus()
	ta.KeyMap.InsertNewline = key.NewBinding(key.WithKeys("ctrl+j"))
	return MultiInput{ta: ta}
// §foot page/pkg/tui/multinput.go NewMultiInput