// §head page/pkg/cli/cli_test.go:67-118 TestRunStream
// §sig func TestRunStream(t *testing.T)
	dir := t.TempDir()
	socketPath := filepath.Join(dir, "run.sock")

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

		// Read request
		var req jsonRequest
		if err := json.NewDecoder(conn).Decode(&req); err != nil {
			return
		}
		if req.Method != "run" {
			return
		}

		// Send stream response — two lines then done
		writeStream := func(typ, data string) {
			s := jsonStream{ID: req.ID, Type: typ, Data: data}
			d, _ := json.Marshal(s)
			fmt.Fprintf(conn, "%s\n", d)
		}
		writeStream("line", "hello from relay")
		writeStream("line", "processing...")
		writeStream("done", "")
	}()

	c := New(socketPath)
	if err := c.Dial(); err != nil {
		t.Fatal(err)
	}
	defer c.Close()

	// Captures stdout — run the method and check no error
	if err := c.Run("test prompt"); err != nil {
		t.Fatal(err)
	}

	<-serverDone
// §foot page/pkg/cli/cli_test.go TestRunStream