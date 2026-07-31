// §head page/pkg/tui/tui.go:690-727 reverseGroups
// §sig func reverseGroups(views []PodView) []PodView
	if len(views) == 0 {
		return views
	}
	var newBtn *PodView
	if views[0].ID == "+ new" {
		btn := views[0]
		newBtn = &btn
		views = views[1:]
	}
	if len(views) == 0 {
		if newBtn != nil {
			return []PodView{*newBtn}
		}
		return views
	}
	var groups [][]PodView
	i := 0
	for i < len(views) {
		j := i + 1
		for j < len(views) && views[j].Depth > views[i].Depth {
			j++
		}
		groups = append(groups, views[i:j])
		i = j
	}
	for l, r := 0, len(groups)-1; l < r; l, r = l+1, r-1 {
		groups[l], groups[r] = groups[r], groups[l]
	}
	out := make([]PodView, 0, len(views)+1)
	if newBtn != nil {
		out = append(out, *newBtn)
	}
	for _, g := range groups {
		out = append(out, g...)
	}
	return out
// §foot page/pkg/tui/tui.go reverseGroups