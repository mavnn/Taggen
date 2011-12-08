module Taggen.Img
open Taggen.Core

let img alt source =
    FragAttr ("img", (Some ["source", source; "alt", alt]), [])