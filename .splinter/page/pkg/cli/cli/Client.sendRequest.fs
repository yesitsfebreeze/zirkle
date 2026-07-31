// §head page/pkg/cli/cli.go:65-73 Client.sendRequest
// §sig func (c *Client) sendRequest(method string, params map[string]any) error
	req := jsonRequest{ID: 1, Method: method, Params: params}
	data, err := json.Marshal(req)
	if err != nil {
		return err
	}
	_, err = fmt.Fprintln(c.conn, string(data))
	return err
// §foot page/pkg/cli/cli.go Client.sendRequest