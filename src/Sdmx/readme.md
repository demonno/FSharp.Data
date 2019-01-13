# SdmxProvider

## Setup

    git clone https://github.com/demonno/FSharp.Data
    git checkout sdxm-types

## Build

To build project 

## Linux/Mac

    sh build.sh Build

## Windows
    
    * Visual Studio - Open `FSharp.Data.sln` Click Rebuild all projects
    * CMD - Run `build.cmd`
    
## Test

Open new IDE or Editor, and refference `bin/lib/net45/FSharp.Data.dll`

Exmaples folder contains usecases:

    https://github.com/demonno/FSharp.Data/tree/sdmx-types/src/Sdmx/examples 


Example and Comparision of WorldBank and SDMX type providers
```fsharp
#r @"../../../../bin/lib/net45/FSharp.Data.dll"
#r "System.Xml.Linq.dll"
#load @"../../../../packages/test/FSharp.Charting/FSharp.Charting.fsx"
open FSharp.Data
open FSharp.Charting

// WorldBank Provider For Comparision
let data = WorldBankData.GetDataContext()
let wbData = data.Countries.``United Kingdom``.Indicators.``Gross capital formation (% of GDP)``

// SDMX version
type WB = SdmxDataProvider<"https://api.worldbank.org/v2/sdmx/rest">
type WDI = WB.``World Development Indicators``

let sdmxData = WDI(WDI.Frequency.Monthly_M,
               WDI.``Reference Area``.``United Kingdom_GBR``,
               WDI.Series.``Gross capital formation (% of GDP)_NE_GDI_TOTL_ZS``)

let wch = wbData |> Chart.Line
let sch = sdmxData |> Chart.Line
Chart.Combine( [Chart.Line(sdmxData); Chart.Line(wbData)] )

```

