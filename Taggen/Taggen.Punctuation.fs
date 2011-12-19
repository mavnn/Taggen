module Taggen.Punctuation
open Taggen.Core
open Taggen.Utils

/// Create Fragment without children
let (!<>) tagName =
    Frag (tagName, [])

/// Add child to fragment (as last child)
let (^<>) fragment child =
    addChild child fragment

/// Add child to fragment (as last child)
let (+<>) fragment child =
    addChild child fragment

/// Add child to fragment (as last child)
let (|<>) fragment child =
    addChild child fragment

/// Add inner text to fragment
let (+~) fragment text =
    addText text fragment

/// Add inner text to fragment
let (+) fragment text =
    addText text fragment

/// Add inner text to fragment
let (%) fragment text =
    addText text fragment

/// Add list of children to fragment
let (^<<) fragment newChildren =
    addChildList newChildren fragment

/// Add list of children to fragment
let (+<<) fragment newChildren =
    addChildList newChildren fragment

/// Create string representation of Fragment
let (!) fragment =
    printFrag fragment

/// Create nicely formatted human readable string representation of Fragment
let (!!) fragment =
    prettyPrint fragment

/// Add classes or id to existing fragment using ".class#id" syntax
let (+.) fragment classes =
    addClassOrId classes fragment 

/// Add attributes as list of string, string tuples
let (++) fragment attrs =
    addAttrs attrs fragment
