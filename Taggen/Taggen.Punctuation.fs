module Taggen.Punctuation
open Taggen.Core
open Taggen.Utils

/// Create Fragment without children
let (!<>) tagName =
    Frag (tagName, [])

/// Add child to fragment (as last child)
let (<>+) fragment child =
    match fragment with
    | Frag (tag, children) -> Frag (tag, List.append children [child])
    | FragAttr (tag, attr, children) -> FragAttr (tag, attr, List.append children [child])
    | Text _ -> failwith "You cannot add a child fragment to a Text fragment."

/// Add inner text to fragment
let (+~) (fragment : Fragment) text =
    fragment <>+ (Text text)

/// Add list of children to fragment
let (<<+) fragment newChildren =
    match fragment with
    | Frag (tag, children) -> Frag (tag, List.append children newChildren)
    | FragAttr (tag, attr, children) -> FragAttr (tag, attr, List.append children newChildren)
    | Text _ -> failwith "You cannot add children to a Text fragment."

/// Create string representation of Fragment
let (!) fragment =
    printFrag fragment

/// Create nicely formatted human readable string representation of Fragment
let (!!) fragment =
    prettyPrint fragment

/// Add classes or id to existing fragment using ".class#id" syntax
let (+.) fragment classes =
    match fragment with
    | Frag (tag, children) -> Frag(tag + classes, children)
    | FragAttr (tag, attr, children) -> FragAttr(tag + classes, attr, children)
    | Text _ -> failwith "You cannot add classes or id to a Text fragment."

/// Add attributes as list of string, string tuples
let (++) fragment attrs =
    match fragment with
    | Frag (tag, children) -> FragAttr(tag, Some attrs, children)
    | FragAttr (tag, attr, children) -> FragAttr(tag, Some attrs, children)
    | Text _ -> failwith "You cannot add attributes to a Text fragment."