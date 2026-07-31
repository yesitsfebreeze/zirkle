// §head page/pkg/scheduler/scheduler.go:68-72 cronJob.Run
// §sig func (j cronJob) Run()
	// context.Background(): cron.Run carries no context. The Trigger owns
	// any per-turn deadline (e.g. the agent's token budget / turn timeout).
	_ = j.t.Fire(context.Background(), j.agentID)
// §foot page/pkg/scheduler/scheduler.go cronJob.Run