// §head page/pkg/tui/tui_test.go:542-559 TestSymbolForState
// §sig func TestSymbolForState(t *testing.T)
	tests := map[string]string{
		"done":     "■",
		"stopped":  "■",
		"running":  "▶",
		"created":  "●",
		"waiting":  "●",
		"planning": "●",
		"failed":   "✕",
		"stuck":    "✕",
		"ready":    "+",
	}
	for state, want := range tests {
		if got := symbolForState(state); got != want {
			t.Errorf("symbolForState(%q) = %q, want %q", state, got, want)
		}
	}
// §foot page/pkg/tui/tui_test.go TestSymbolForState