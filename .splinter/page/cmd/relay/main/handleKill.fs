// §head page/cmd/relay/main.go:570-581 handleKill
// §sig func handleKill(conn net.Conn, id int, params map[string]any, s store.Store)
	podID, _ := params["id"].(string)
	if podID == "" {
		sendRPCError(conn, id, "missing id")
		return
	}
	if err := s.Delete(podID); err != nil {
		sendRPCError(conn, id, err.Error())
		return
	}
	sendRPCResult(conn, id, "killed")
// §foot page/cmd/relay/main.go handleKill