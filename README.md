# Utilities

### Automation
My attempt at automating the Kroger Fuel Points Feedback before it was discontinued in my area (possibly due to the coronavirus?). Using Selenium, the program takes a couple of inputs and then goes through every form eventually submitting with my Loyalty Card ID. I wasn't able to figure out the datepicker with Selenium before the discontinuation. 


### CryptoData
This project makes two calls out to Crypto Compare for crypto and IEX for stocks. It then uploads the data to an excel spreadsheet (and the excel spreadsheet does some calculations itself) for display. It is currently deployed as a scheduled task and executes at every log in. Planning to expand it, especially the IEX call, for a more comprehensive and visual overview of my current portfolio.
