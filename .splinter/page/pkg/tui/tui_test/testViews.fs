// §head page/pkg/tui/tui_test.go:26-32 testViews
// §sig func testViews() []PodView
	return []PodView{
		{ID: "alpha", Prompt: "do thing", Mode: "smart", State: "running", Depth: 0, HasChildren: true},
		{ID: "child1", Prompt: "sub task", Mode: "quick", State: "running", Depth: 1, HasChildren: false},
		{ID: "beta", Prompt: "do other", Mode: "rush", State: "done", Depth: 0, HasChildren: false},
	}
// §foot page/pkg/tui/tui_test.go testViews