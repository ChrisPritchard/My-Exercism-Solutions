package erratum

// Use opens the given resource opener
func Use(o ResourceOpener, input string) (err error) {
	opener, err := o()
	for err != nil {
		if _, ok := err.(TransientError); !ok {
			return err
		}
		opener, err = o()
	}
	defer opener.Close()

	defer func() {
		recovered := recover()
		if recovered == nil {
			return
		}

		if frobError, ok := recovered.(FrobError); ok {
			opener.Defrob(frobError.defrobTag)
		}
		err = recovered.(error)
	}()
	opener.Frob(input)

	return
}
