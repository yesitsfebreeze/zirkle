// §head page/pkg/tui/render.go:106-115 Model.listContent
// §sig func (m Model) listContent() string
	if m.help {
		items := m.helpListItems()
		return RenderList(items, m.helpCur, m.helpDetail, m.vp.Width)
	}
	if m.config {
		return m.renderConfig()
	}
	return m.renderTree()
// §foot page/pkg/tui/render.go Model.listContent