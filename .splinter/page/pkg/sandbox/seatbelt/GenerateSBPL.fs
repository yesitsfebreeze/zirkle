// §head page/pkg/sandbox/seatbelt.go:16-59 GenerateSBPL
// §sig func GenerateSBPL(s Spec) string
	var b strings.Builder
	b.WriteString("(version 1)\n")
	b.WriteString("(deny default)\n")

	// Read: deny-then-allow. Tools are read-only; /dev and /tmp are needed
	// for the process to function.
	tools := s.Tools
	if tools == nil {
		tools = DefaultTools
	}
	for _, t := range tools {
		fmt.Fprintf(&b, "(allow file-read* (subpath %q))\n", t)
	}
	b.WriteString("(allow file-read* (subpath \"/dev\"))\n")
	b.WriteString("(allow file-read* (subpath \"/tmp\"))\n")

	// Process execution from tool paths, plus fork and self-signal so
	// subprocess spawning works.
	for _, t := range tools {
		fmt.Fprintf(&b, "(allow process-exec (subpath %q))\n", t)
	}
	b.WriteString("(allow process-fork)\n")
	b.WriteString("(allow signal (target self))\n")

	// Write: only the spec's paths.
	if s.Dir != "" {
		fmt.Fprintf(&b, "(allow file-write* (subpath %q))\n", s.Dir)
	}
	b.WriteString("(allow file-write* (subpath \"/tmp\"))\n")
	for _, p := range s.RW {
		fmt.Fprintf(&b, "(allow file-write* (subpath %q))\n", p)
	}

	// Network: denied unless the spec says otherwise. The egress proxy is
	// a unix socket (file I/O), not a network endpoint.
	if s.Net {
		b.WriteString("(allow network*)\n")
	} else {
		b.WriteString("(deny network*)\n")
	}

	return b.String()
// §foot page/pkg/sandbox/seatbelt.go GenerateSBPL