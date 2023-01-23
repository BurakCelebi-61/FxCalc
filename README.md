
## SimpleCalculation Basic Example

```c#
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>();
            calculation.Formule = "a*((b*c)/a)*d+(c-d*(c,a))";
            calculation.SetVarible("a", "43");
            calculation.SetVarible("b", "200");
            calculation.SetVarible("c", "-15");
            calculation.SetVarible("d", "0,054");
            calculation.Compute();
```
#### SimpleCalculation Basic Example Result 1
```c#
            var result = calculation.Result;
```
#### SimpleCalculation Basic Example Result 2
```c#
            var result = calculation.Compute();
```
#### SimpleCalculation Specifying the data entry type
```c#
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>(FxCalc.Enums.InputType.Double);
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>(FxCalc.Enums.InputType.Decimal);
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>(FxCalc.Enums.InputType.Int);
```
#### SimpleCalculation Result Type options
```c#
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>();
            SimpleCalculation<double> calculation = new SimpleCalculation<double>();
            SimpleCalculation<int> calculation = new SimpleCalculation<int>();
```
