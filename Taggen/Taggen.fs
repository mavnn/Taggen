module Taggen
open System.Text.RegularExpressions
open System.Collections

let RegMatch pattern input =
    let matches = Regex.Matches(input, pattern)
    matches |> Seq.cast
    |> Seq.map (fun (m : Match) ->
        [for g in m.Groups -> (g.Value)])

let inline foldStrings strings = Seq.fold (fun acc item -> acc + item) "" strings

let writeTagname tagText = 
    RegMatch (@"\w+") tagText
    |> Seq.head 
    |> List.head

let rec writeClasses tagText =
    let classes = RegMatch (@"\.\w+") tagText |> Seq.concat |> Seq.map (fun dottedName -> dottedName.Trim '.')
    match Seq.length classes with
    | 0 -> ""
    | 1 -> sprintf " class=\"%s\"" (Seq.head classes)
    | _ -> Seq.fold (fun acc item -> acc + " " + item) (Seq.head classes) (Seq.skip 1 classes) |> sprintf " class=\"%s\""

let writeId tagText =
    let matches = RegMatch (@"#\w+") tagText
    if (Seq.length matches > 1) then failwith "An element can only have one id"
    else
        Seq.map (fun groups -> groups |> List.map (fun (x : string) -> sprintf " id=\"%s\"" (x.Trim('#'))) |> foldStrings) matches |> foldStrings

type Fragment =
    | Text of string
    | Frag of string * (Fragment seq)

let rec printFrag fragment =
    match fragment with
    | Text t -> t
    | Frag (tag, children) ->
        let tagName = writeTagname tag
        let classes = writeClasses tag
        let id = writeId tag
        let openContents = sprintf "%s%s%s" tagName classes id
        let innerText = Seq.map printFrag children |> foldStrings
        if children = Seq.empty then
            sprintf "<%s/>" openContents
        else
            sprintf "<%s>%s</%s>" openContents innerText tagName
