module Taggen.Links
open Taggen.Core

let link text location =
    FragAttr ("a", Some([("href", location)]), [Text text])