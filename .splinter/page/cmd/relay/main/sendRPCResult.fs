// §head page/cmd/relay/main.go:624-628 sendRPCResult
// §sig func sendRPCResult(conn net.Conn, id int, result any)
	resp := rpcResponse{ID: id, Result: result}
	data, _ := json.Marshal(resp)
	fmt.Fprintf(conn, "%s\n", data)
// §foot page/cmd/relay/main.go sendRPCResult