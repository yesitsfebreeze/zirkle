// §head page/pkg/tui/fuzz_test.go:11-38 keyFor
// §sig func keyFor(b byte) tea.KeyMsg
	switch b % 12 {
	case 0:
		return tea.KeyMsg{Type: tea.KeyLeft}
	case 1:
		return tea.KeyMsg{Type: tea.KeyRight}
	case 2:
		return tea.KeyMsg{Type: tea.KeyUp}
	case 3:
		return tea.KeyMsg{Type: tea.KeyDown}
	case 4:
		return tea.KeyMsg{Type: tea.KeyEnter}
	case 5:
		return tea.KeyMsg{Type: tea.KeyBackspace}
	case 6:
		return tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{'q'}}
	case 7:
		return tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{'k'}}
	case 8:
		return tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{'j'}}
	case 9:
		return tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{'x'}}
	case 10:
		return tea.KeyMsg{Type: tea.KeyEsc}
	default:
		return tea.KeyMsg{Type: tea.KeySpace}
	}
// §foot page/pkg/tui/fuzz_test.go keyFor