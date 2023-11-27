# CurrencyConvertor
## General information
Projects structure: 
- Bckend. 
    - Path to project: src\CurrencyConvertor.API
    - ASP.NET Core 8.0
    - Host: https://localhost:7209
- Web client application 
    - Path to project: src\CurrencyConvertor.WebUI
    - Angular 17.0
    - Host: http://localhost:4200/

# Implementation Notes

## TODOs
Several TODOs and comments were added to the code. Yes, I didn't forget to complete these TODOs. They are there to highlight improvements that need to be finished in a real-world project before release.

## Architectural Decision Records (ADRs)
The intention of using IConverterFactory and IConverter was to make the solution more flexible. The conversion of values for different currencies and languages can be challenging. From what I know, in some languages, currencies can have different genders, and different rules may be used to convert them into words. Most likely, there are languages with even more complex rules.

## Room for improvement
Also, it is possible to use external services to convert values to words and/or currencies. For this purpose, a Converter utilizing external services may be implemented. When using external services, it is necessary to consider caching (in memory, distributed, or database) to speed up conversion and/or reduce the cost of using external services. However, it is possible that the cache will not be useful, as there are a large number of potential combinations of values/languages/currencies. To determine if a cache mechanism is needed, more details about converter use cases are required.