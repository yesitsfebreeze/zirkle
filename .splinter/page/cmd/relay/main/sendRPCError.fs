// §head page/cmd/relay/main.go:630-634 sendRPCError
// §sig func sendRPCError(conn net.Conn, id int, errMsg string)
	resp := rpcResponse{ID: id, Error: errMsg}
	data, _ := json.Marshal(resp)
	fmt.Fprintf(conn, "%s\n", data)
// §foot page/cmd/relay/main.go sendRPCError