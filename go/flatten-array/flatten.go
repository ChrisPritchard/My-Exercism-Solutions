package flatten

// Flatten takes an arbitarily deep nested slice and returns a flat slice of its contents
func Flatten(input interface{}) []interface{} {
	result := []interface{}{}
	switch input.(type) {
	case []interface{}:
		{
			list := input.([]interface{})
			for _, v := range list {
				result = append(result, Flatten(v)...)
			}
		}
	case interface{}:
		return []interface{}{input}
	}
	return result
}
