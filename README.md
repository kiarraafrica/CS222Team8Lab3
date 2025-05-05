# DigitalDiary

# 📘**Object-Oriented Programming (OOP) Principles in This Project**

This diary application leverages the core principles of Object-Oriented Programming (OOP)
    
— Encapsulation, Abstraction, Inheritance, and Polymorphism — to build a clean, extensible, and maintainable codebase.


🔐**Encapsulation**

* Encapsulation is achieved by grouping related data and methods inside classes like Diary and SecureDiary. Internal file operations (ReadAllLinesToList, WriteLinesToFile) are hidden from external access and exposed through public methods such as WriteEntry, EditEntry, and DeleteEntry.

* The password in SecureDiary is stored in a private field (_password) and accessed internally, ensuring it's not exposed to outside classes.


🎯 **Abstraction**

* Abstraction is demonstrated through the use of the IDiary interface.

* IDiary defines essential diary operations like WriteEntry, GetAllEntriesAsList, SearchByDate, etc., without exposing implementation details.

* Consumers of the diary interact with the diary through the IDiary interface, allowing flexibility in swapping implementations.
  

🧬 **Inheritance**

* Inheritance is used by extending the base Diary class into a more specialized SecureDiary.

* SecureDiary inherits standard diary functionality and overrides only the necessary parts to add password protection (e.g., reading/writing entries).

* This prevents code duplication and promotes reuse.

🔁 **Polymorphism**

* Polymorphism is utilized through:

* Method overriding in SecureDiary (ReadAllLinesToList, WriteLinesToFile) to change behavior while preserving the interface.

* The ability to reference both Diary and SecureDiary using the IDiary interface, enabling flexible and interchangeable diary usage.
