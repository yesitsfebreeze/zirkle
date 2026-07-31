// §head page/cmd/relay/main.go:49-107 podSource.List
// §sig func (s *podSource) List() ([]tui.PodView, error)
	pods, err := s.store.List()
	if err != nil {
		return nil, err
	}
	newBtn := tui.PodView{
		ID:     "+ new",
		Prompt: "type prompt below to start a new pod",
		Mode:   "smart",
		State:  "ready",
		Recap:  "start new pod",
	}
	if len(pods) == 0 {
		out := make([]tui.PodView, 0, len(mockTree)+1)
		out = append(out, newBtn)
		out = append(out, mockTree...)
		return out, nil
	}
	ids := make([]string, 0, len(pods))
	for _, o := range pods {
		ids = append(ids, o.ID)
	}
	children, err := s.store.ExecutionsByParents(ids)
	if err != nil {
		return nil, err
	}
	out := make([]tui.PodView, 0, len(pods)*2+1)
	out = append(out, newBtn)
	for _, o := range pods {
		kids := children[o.ID]
		out = append(out, tui.PodView{
			ID:          o.ID,
			Prompt:      o.Prompt,
			Mode:        o.Mode,
			State:       o.State,
			Recap:       o.Recap,
			CreatedAt:   o.CreatedAt,
			Depth:       0,
			HasChildren: len(kids) > 0,
		})
		// Subpods nest under their parent: one row per recorded execution.
		for _, k := range kids {
			state := "done"
			if !k.Success {
				state = "failed"
			}
			out = append(out, tui.PodView{
				ID:        "subpod:" + strconv.FormatInt(k.ID, 10),
				Prompt:    k.Prompt,
				Mode:      k.Model,
				State:     state,
				Recap:     k.Summary,
				CreatedAt: k.CreatedAt,
				Depth:     1,
			})
		}
	}
	return out, nil
// §foot page/cmd/relay/main.go podSource.List