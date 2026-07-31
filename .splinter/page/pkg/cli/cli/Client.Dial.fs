// §head page/pkg/cli/cli.go:48-55 Client.Dial
// §sig func (c *Client) Dial() error
	conn, err := net.Dial("unix", c.SocketPath)
	if err != nil {
		return fmt.Errorf("cannot connect to daemon at %s: %w", c.SocketPath, err)
	}
	c.conn = conn
	return nil
// §foot page/pkg/cli/cli.go Client.Dial