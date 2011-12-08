module Taggen.Text
open Taggen.Core

let p text =
    Frag ("p", [Text text])