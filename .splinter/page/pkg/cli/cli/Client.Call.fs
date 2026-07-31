// §head page/pkg/cli/cli.go:77-97 Client.Call
// §sig func (c *Client) Call(method string, params any) (any, error)
	paramsMap := toMap(params)
	if err := c.sendRequest(method, paramsMap); err != nil {
		return nil, err
	}
	scanner := bufio.NewScanner(c.conn)
	if !scanner.Scan() {
		if err := scanner.Err(); err != nil {
			return nil, err
		}
		return nil, fmt.Errorf("connection closed")
	}
	var resp jsonResponse
	if err := json.Unmarshal(scanner.Bytes(), &resp); err != nil {
		return nil, err
	}
	if resp.Error != "" {
		return nil, fmt.Errorf("%s", resp.Error)
	}
	return resp.Result, nil
// §foot page/pkg/cli/cli.go Client.Call