// §head page/cmd/relay/main.go:552-568 handleList
// §sig func handleList(conn net.Conn, id int, s store.Store)
	pods, err := s.List()
	if err != nil {
		sendRPCError(conn, id, err.Error())
		return
	}
	result := make([]map[string]any, 0, len(pods))
	for _, o := range pods {
		result = append(result, map[string]any{
			"ID":    o.ID,
			"State": o.State,
			"Mode":  o.Mode,
			"Recap": o.Recap,
		})
	}
	sendRPCResult(conn, id, result)
// §foot page/cmd/relay/main.go handleList