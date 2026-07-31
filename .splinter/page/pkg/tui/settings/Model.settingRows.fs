// §head page/pkg/tui/settings.go:35-45 Model.settingRows
// §sig func (m Model) settingRows() []settingRow
	return []settingRow{
		{kind: settingColor, label: "Highlight Color", color: m.highlightColor},
		{kind: settingColor, label: "Attention Color", color: m.attentionColor},
		{kind: settingToggle, label: "Timeline Headers", on: m.tl.Enabled},
		{kind: settingToggle, label: "Pod Count", on: m.tl.ShowCount},
		{kind: settingToggle, label: "State Tallies", on: m.tl.ShowStates},
		{kind: settingToggle, label: "Time Span", on: m.tl.ShowSpan},
		{kind: settingChoice, label: "Frame", value: m.tl.Frame, opts: frameOptions},
	}
// §foot page/pkg/tui/settings.go Model.settingRows