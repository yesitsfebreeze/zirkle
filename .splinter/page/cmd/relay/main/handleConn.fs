// §head page/cmd/relay/main.go:486-506 handleConn
// §sig func handleConn(conn net.Conn, s store.Store, l llm.LLM)
	defer conn.Close()

	var req rpcRequest
	if err := json.NewDecoder(conn).Decode(&req); err != nil {
		return
	}

	switch req.Method {
	case "run":
		handleRun(conn, req.ID, req.Params, s, l)
	case "list":
		handleList(conn, req.ID, s)
	case "kill":
		handleKill(conn, req.ID, req.Params, s)
	case "logs":
		handleLogs(conn, req.ID, req.Params, s)
	default:
		sendRPCError(conn, req.ID, "unknown method: "+req.Method)
	}
// §foot page/cmd/relay/main.go handleConn