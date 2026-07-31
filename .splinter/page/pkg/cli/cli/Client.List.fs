// §head page/pkg/cli/cli.go:123-144 Client.List
// §sig func (c *Client) List() error
	result, err := c.Call("list", nil)
	if err != nil {
		return err
	}
	pods, _ := result.([]any)
	if len(pods) == 0 {
		fmt.Println("no pods")
		return nil
	}
	w := tabwriter.NewWriter(os.Stdout, 0, 0, 3, ' ', 0)
	fmt.Fprintln(w, "ID\tSTATE\tMODE\tRECAP")
	for _, pod := range pods {
		m, _ := pod.(map[string]any)
		id, _ := m["ID"].(string)
		state, _ := m["State"].(string)
		mode, _ := m["Mode"].(string)
		recap, _ := m["Recap"].(string)
		fmt.Fprintf(w, "%s\t%s\t%s\t%s\n", id, state, mode, recap)
	}
	return w.Flush()
// §foot page/pkg/cli/cli.go Client.List