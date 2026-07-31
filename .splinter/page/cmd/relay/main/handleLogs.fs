// §head page/cmd/relay/main.go:583-620 handleLogs
// §sig func handleLogs(conn net.Conn, id int, params map[string]any, s store.Store)
	podID, _ := params["id"].(string)
	if podID == "" {
		sendStream(conn, id, "error", "missing id")
		return
	}
	o, err := s.Load(podID)
	if err != nil {
		sendStream(conn, id, "error", "not found: "+err.Error())
		return
	}

	sendStream(conn, id, "line", fmt.Sprintf("ID: %s", o.ID))
	sendStream(conn, id, "line", fmt.Sprintf("Prompt: %s", o.Prompt))
	sendStream(conn, id, "line", fmt.Sprintf("State: %s", o.State))
	sendStream(conn, id, "line", fmt.Sprintf("Recap: %s", o.Recap))

	// Dump checkpoint history
	turn := 1
	for {
		data, err := s.LoadCheckpoint(podID, turn)
		if err != nil {
			break
		}
		var msgs []llm.Message
		if err := json.Unmarshal(data, &msgs); err == nil {
			for _, msg := range msgs {
				role := msg.Role
				if role == "" {
					role = "?"
				}
				sendStream(conn, id, "line", fmt.Sprintf("[%s] %s", role, msg.Content))
			}
		}
		turn++
	}
	sendStream(conn, id, "done", "")
// §foot page/cmd/relay/main.go handleLogs