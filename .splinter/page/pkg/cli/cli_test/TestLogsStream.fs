// §head page/pkg/cli/cli_test.go:198-241 TestLogsStream
// §sig func TestLogsStream(t *testing.T)
	dir := t.TempDir()
	socketPath := filepath.Join(dir, "logs.sock")

	lis, err := net.Listen("unix", socketPath)
	if err != nil {
		t.Fatal(err)
	}
	defer lis.Close()

	serverDone := make(chan struct{})
	go func() {
		defer close(serverDone)
		conn, err := lis.Accept()
		if err != nil {
			return
		}
		defer conn.Close()

		var req jsonRequest
		json.NewDecoder(conn).Decode(&req)

		writeStream := func(typ, data string) {
			s := jsonStream{ID: req.ID, Type: typ, Data: data}
			d, _ := json.Marshal(s)
			fmt.Fprintf(conn, "%s\n", d)
		}
		writeStream("line", "ID: test-pod")
		writeStream("line", "Prompt: hello")
		writeStream("line", "State: done")
		writeStream("done", "")
	}()

	c := New(socketPath)
	if err := c.Dial(); err != nil {
		t.Fatal(err)
	}
	defer c.Close()

	if err := c.Logs("test-pod"); err != nil {
		t.Fatal(err)
	}
	<-serverDone
// §foot page/pkg/cli/cli_test.go TestLogsStream