// §head page/pkg/cli/cli.go:40-45 New
// §sig func New(socketPath string) *Client
	if socketPath == "" {
		socketPath = DefaultSocketPath
	}
	return &Client{SocketPath: socketPath}
// §foot page/pkg/cli/cli.go New