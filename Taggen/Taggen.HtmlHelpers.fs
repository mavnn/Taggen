module Taggen.HtmlHelpers
open Taggen.Core
open Taggen.Punctuation

/// <summary>Image tag helper</summary>
/// <param name="alt">Alternative text</param>
/// <param name="source">Location of image file</param>
let img alt source =
    FragAttr ("img", (Some ["src", source; "alt", alt]), [])

/// Creates a paragraph tag with inner text of 'text'
let p text =
    Frag ("p", [Text text])

// Link helpers
let linkTo text location =
    FragAttr ("a", Some([("href", location)]), [Text text])

let mailTo email =
    FragAttr ("a", Some([("href", email)]), [Text email])