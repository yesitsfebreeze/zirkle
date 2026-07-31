// §head page/cmd/relay/main.go:636-640 sendStream
// §sig func sendStream(conn net.Conn, id int, typ, data string)
	s := rpcStream{ID: id, Type: typ, Data: data}
	d, _ := json.Marshal(s)
	fmt.Fprintf(conn, "%s\n", d)
// §foot page/cmd/relay/main.go sendStream