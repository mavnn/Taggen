# Taggen

## Light weight HTML generation in F#

A simple DSL for generating HTML and XML within F#. Still under heavy development for a personal project.

A strongly typed page template might look like this:

``` fsharp
let pageFramework pageTitle navItems content =
    Frag("html",
            [
                Frag("header",
                        [Frag("title", [Text pageTitle])]
                    )
                Frag("body",
                        [
                            Frag("nav", navItems)
                            Frag("content", content)
                        ]
                    )
            ]
        )
```


Check out Program.fs in the TaggenScratchPad project for examples of it's current state, including experiments with various helper methods and styles.

### Todo

* Write more performant foldStrings method
* Experiment with right and left associative infix 'punctuation' to ease fragment creation
* ? use Manos as default host/get routing etc?
* Finish list of html tag types
* Add doctype helpers