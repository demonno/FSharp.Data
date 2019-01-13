#r @"../../../bin/lib/net45/FSharp.Data.dll"
#load @"../../../packages/test/FSharp.Charting/FSharp.Charting.fsx"
open FSharp.Data
open FSharp.Charting

// ECB Example 1
type ECB = SdmxDataProvider<"http://a-sdw-wsrest.ecb.int/service">
type EXR = ECB.``Exchange Rates``
let ecbData = EXR(EXR.Frequency.Annual_A,
                  EXR.Currency.``US dollar_USD``,
                  EXR.``Currency denominator``.Euro_EUR,
                  EXR.``Exchange rate type``.Spot_SP00,
                  EXR.``Series variation - EXR context``.Average_A)
ecbData.Data |> Chart.Line

// ECB Example 2

type BSI = ECB.``Balance Sheet Items``
let getLoanData creditDimension = BSI(
      BSI.Frequency.Monthly_M,
      BSI.``Counterpart area``.``Euro area (changing composition)_U2``,
      BSI.``Adjustment indicator``.``Working day and seasonally adjusted_Y``,
      BSI.``BS reference sector breakdown``.``Monetary and Financial Institutions (MFIs)_U``, 
      creditDimension,
      BSI.``Original maturity``.Total_A,
      BSI.``Data type``.``Financial transactions (flows)_4``,
      BSI.``Reference area``.``Euro area (changing composition)_U2``,
      BSI.``BS counterpart sector``.``Households and non-profit institutions serving households (S.14 and S.15)_2250``,
      BSI.``Currency of transaction``.``All currencies combined_Z01``,
      BSI.``Balance sheet suffix``.Euro_E).Data

let data = [ getLoanData BSI.``Balance sheet item``.``Adjusted loans_A20T`` ;
             getLoanData BSI.``Balance sheet item``.``Credit for consumption_A21``;
             getLoanData BSI.``Balance sheet item``.``Lending for house purchase_A22``;
             getLoanData BSI.``Balance sheet item``.``Other lending_A23``]

// alternative:
let data' = [ BSI("M.U2.Y.U.A20T.A.4.U2.2250.Z01.E").Data ;
              BSI("M.U2.Y.U.A21.A.4.U2.2250.Z01.E").Data ;
              BSI("M.U2.Y.U.A22.A.4.U2.2250.Z01.E").Data ;
              BSI("M.U2.Y.U.A23.A.4.U2.2250.Z01.E").Data ]

open FSharp.Charting

data |> List.map Chart.Line
     |> Chart.Combine
     |> Chart.Show