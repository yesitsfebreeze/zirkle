// §head page/pkg/cli/cli.go:58-63 Client.Close
// §sig func (c *Client) Close() error
	if c.conn != nil {
		return c.conn.Close()
	}
	return nil
// §foot page/pkg/cli/cli.go Client.Close