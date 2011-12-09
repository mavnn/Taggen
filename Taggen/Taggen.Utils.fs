module Taggen.Utils
open Taggen.Core
open System.IO
open System.Xml

let prettyPrint frag =
    let stringRep = printFrag frag
    let doc = new XmlDocument ()
    doc.LoadXml(stringRep)
    let mem = new MemoryStream ()
    let xmlWriter = new XmlTextWriter (mem, System.Text.Encoding.UTF8)
    xmlWriter.Formatting <- Formatting.Indented
    doc.WriteTo xmlWriter
    xmlWriter.Flush ()
    mem.Seek (int64 0, SeekOrigin.Begin) |> ignore
    let reader = new StreamReader (mem)
    reader.ReadToEnd ()

