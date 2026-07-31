// §head page/pkg/sandbox/sandbox.go:120-122 Spec.Command
// §sig func (s Spec) Command(ctx context.Context, argv ...string) (*exec.Cmd, error)
	return activeBackend.Command(ctx, s, argv...)
// §foot page/pkg/sandbox/sandbox.go Spec.Command