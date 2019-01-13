#r @"../../../bin/lib/net45/FSharp.Data.dll"
#load @"../../../packages/test/FSharp.Charting/FSharp.Charting.fsx"
open FSharp.Data
open FSharp.Charting

// WorldBank
type WB = SdmxDataProvider<"https://api.worldbank.org/v2/sdmx/rest">
type WDI = WB.``World Development Indicators``

let data = WDI(WDI.Frequency.Monthly_M,
               WDI.``Reference Area``.``United Kingdom_GBR``,
               WDI.Series.``Gross capital formation (% of GDP)_NE_GDI_TOTL_ZS``)
let data2 = WDI(WDI.Frequency.Monthly_M,
               WDI.``Reference Area``.``United Kingdom_GBR``,
               WDI.Series.``Agricultural land (sq. km)_AG_LND_AGRI_K2``)

data.Data |> Chart.Line