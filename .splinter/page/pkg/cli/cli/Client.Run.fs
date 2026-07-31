// §head page/pkg/cli/cli.go:100-120 Client.Run
// §sig func (c *Client) Run(prompt string) error
	if err := c.sendRequest("run", map[string]any{"prompt": prompt}); err != nil {
		return err
	}
	scanner := bufio.NewScanner(c.conn)
	for scanner.Scan() {
		var s jsonStream
		if err := json.Unmarshal(scanner.Bytes(), &s); err != nil {
			return err
		}
		switch s.Type {
		case "line":
			fmt.Println(s.Data)
		case "done":
			return nil
		case "error":
			return fmt.Errorf("%s", s.Data)
		}
	}
	return scanner.Err()
// §foot page/pkg/cli/cli.go Client.Run