This project was developed as an OOP practice task with the goals of:

* applying OOP: inheritance and polymorphism techniques, ensuring it follows SOLID principles
* implementing validation logic
* using exception handling correctly

### Input validation

* rejects invalid characters
* prevents malformed expressions
* stops consecutive operators
* checks for empty input
* handles decimal separators
* validates division by zero

### The system throws and handles:

* `InvalidOperationException` for unknown operators
* `FormatException` for malformed input
* `DivideByZeroException` for division by zero
* generic fallback exceptions

All exceptions are caught in the UI layer to prevent program crashes.

The UI does not contain calculation logic, parsing logic or validation rules.

I successfully implemented each requirement, trying to write clean and well-structured code.
