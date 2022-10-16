using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ListElement : MonoBehaviour
{
    private void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ListView list = root.Q<ListView>();
        VisualTreeAsset person = Resources.Load<VisualTreeAsset>("Person");
        
        List<Person> people = new List<Person>{
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
            new Person("Harry", "Potter"),
            new Person("Peter", "Parker"),
            new Person("Mary", "Jane"),
            new Person("Tony", "Stark"),
        };

       

        //What is the data it will be displayed
        list.itemsSource = people;
        //How should the list item looks like? In this case, Label. Could be our custom one!
        list.makeItem = () => person.Instantiate();
        //Here the listElement is each one of the elements inside the list. We can access its parameters.
        list.bindItem = (listElement, index) => 
        {
            Label name = listElement.Q<Label>("name");
            Label surname = listElement.Q<Label>("surname");

            name.text = people[index].name;
            surname.text = people[index].surname;
        };

        list.fixedItemHeight = 50;
    }
}

public class Person 
{
    public string name;
    public string surname;
    public Person(string name, string surname) 
    {   
        this.name = name;
        this.surname = surname;
    }
}
