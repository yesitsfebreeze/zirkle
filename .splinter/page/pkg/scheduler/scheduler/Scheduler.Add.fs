// §head page/pkg/scheduler/scheduler.go:45-53 Scheduler.Add
// §sig func (s *Scheduler) Add(spec string, agentID string, t Trigger) error
	if t == nil {
		return fmt.Errorf("scheduler: nil trigger for agent %q", agentID)
	}
	if _, err := s.cron.AddJob(spec, cronJob{agentID: agentID, t: t}); err != nil {
		return fmt.Errorf("scheduler: add %q for agent %q: %w", spec, agentID, err)
	}
	return nil
// §foot page/pkg/scheduler/scheduler.go Scheduler.Add