# QLinspector

Finding Java gadget chains with CodeQL.
More information in our [article](https://www.synacktiv.com/publications/finding-gadgets-like-its-2022.html)

## Installation

Clone the repo
```sh
$ git clone https://github.com/synacktiv/QLinspector.git
```

Search for gadgets:
```sh
$ codeql database analyze log4j --format=sarif-latest --output=log4j.sarif --search-path=./QLinspector/ synacktiv/qlinspector-java-queries
```

## Queries
### `QLinspector.ql`

The main CodeQL query that can be used to find gadget chains.

here is an example with the Aspectj gadget chain:

![aspectj](img/aspectj.png)

Running the above query can sometimes return a lot of false positives. To filter them the `GadgetSanitizer` class has been added. You can add conditions to filter out `DataFlow::Node`:

```ql
/**
 * placeholder for adding sanitizing steps
*/
class GadgetSanitizer extends DataFlow::Node {
  GadgetSanitizer() {
    this.getEnclosingCallable().hasName("")
  }
}
```

### `QLinspectorOld.ql`

Old query that was initially developped. This query do not use the taint model of CodeQL thus it could return different results.

### `BeanFactoryGadgetFinder.ql`

A query that can be used to find new gadget chains based on the `org.apache.naming.factory.BeanFactory`. The `BeanFactory` class, allows to create an instance of arbitrary class with default constructor and call any public method with one `String` parameter.

More information in this blogpost: https://www.veracode.com/blog/research/exploiting-jndi-injections-java

### `CommonsBeanutilsGadgetFinder.ql`

A query that can be used to find alternatives to the `getOutputProperties` method used in the `CommonsBeanutils` chain.

More information here:
- https://www.praetorian.com/blog/relution-remote-code-execution-java-deserialization-vulnerability/
- https://mogwailabs.de/en/blog/2023/04/look-mama-no-templatesimpl/


### `ObjectFactoryFinder.ql`

A query that can be used to find alternatives to the `org.apache.naming.factory.BeanFactory`. This could be usefull during JNDI exploitation.

More information in this blogpost: https://www.veracode.com/blog/research/exploiting-jndi-injections-java

## Resources

- https://www.synacktiv.com/publications/finding-gadgets-like-its-2015-part-1.html
- https://www.synacktiv.com/publications/finding-gadgets-like-its-2015-part-2.html
- https://www.synacktiv.com/publications/finding-gadgets-like-its-2022.html
- https://www.synacktiv.com/publications/java-deserialization-tricks
- https://www.praetorian.com/blog/relution-remote-code-execution-java-deserialization-vulnerability/
- https://www.veracode.com/blog/research/exploiting-jndi-injections-java
- https://mogwailabs.de/en/blog/2023/04/look-mama-no-templatesimpl/
- https://testbnull.medium.com/return-of-the-rhino-analysis-of-mozillarhino-gadgetchain-also-the-writeup-of-hitb-linkextractor-a2074b4ae624
- https://www.buaq.net/go-53869.html
- https://b1ue.cn/archives/529.html