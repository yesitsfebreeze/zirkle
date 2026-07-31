// §head page/pkg/config/save_test.go:120-124 TestSaveTimelineRejectsEmptyPath
// §sig func TestSaveTimelineRejectsEmptyPath(t *testing.T)
	if err := SaveTimeline("", TimelineConfig{}); err == nil {
		t.Error("empty path must fail")
	}
// §foot page/pkg/config/save_test.go TestSaveTimelineRejectsEmptyPath