// §head page/pkg/cli/cli.go:147-154 Client.Kill
// §sig func (c *Client) Kill(id string) error
	result, err := c.Call("kill", map[string]any{"id": id})
	if err != nil {
		return err
	}
	fmt.Println(result)
	return nil
// §foot page/pkg/cli/cli.go Client.Kill