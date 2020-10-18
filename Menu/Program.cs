using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Menu {
    class Program {
        static void Main(string[] args) {
            MainMenu();
        }

        // Display main menu
        public static void MainMenu() {
            bool inputvalid;
            Console.WriteLine("\n\nWelcome to the Community Library");
            Console.WriteLine("===========Main Menu============");
            Console.WriteLine(" 1. Staff Login");
            Console.WriteLine(" 2. Member Login");
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("================================");
            Console.Write("\nPlease make a selection (1-2, or 0 to exit): ");
            do {
                inputvalid = true;
                switch (Console.ReadLine()) { 
                    case "1":
                        StaffMenu();
                        break;
                    case "2":
                        
                        MemberCollection.MemberMenu();
                        break;
                    case "0":
                        break;
                    default:
                        Console.Write("Invalid input! Please try again: ");
                        inputvalid = false;
                        break;
                }
            } while (inputvalid == false);


        }

        // Display staff menu
        public static void StaffMenu() {
            const string username = "staff"; // staff username
            const string password = "today123"; // staff password
            
            while (true) { 
                Console.Write("Enter username: ");
                if(Console.ReadLine() == username) { // verify the input username equal to staff username 
                    Console.Write("Enter Password: ");
                    if(Console.ReadLine() == password){ // veryfy the input password equal to password
                        while (true) { 
                            Console.WriteLine("\n\n===========Staff Menu============");
                            Console.WriteLine("1. Add a new movie DVD");
                            Console.WriteLine("2. Remove a movie DVD");
                            Console.WriteLine("3. Register a new member");
                            Console.WriteLine("4. Find a registered member's phone number");
                            Console.WriteLine("0. Return to main menu");
                            Console.WriteLine("=====================================");
                            Console.Write("\nPlease make a selection (1-4, or 0 to exit to main menu): ");
                            switch (Console.ReadLine()) {
                                case "1":
                                    MovieCollection.AddMovie();
                                    break;
                                case "2":
                                    MovieCollection.RemoveMovie();
                                    break;
                                case "3":
                                    MemberCollection.Registermember();
                                    break;
                                case "4":
                                    MemberCollection.FindPhoneNum();
                                    break;
                                case "0":
                                    MainMenu();
                                    return;
                                default:
                                    Console.Write("Invalid input! Please try again: ");
                                    break;
                            }
                        }
                    } else { 
                        Console.WriteLine("Incorrect Password!");
                    }
                } else { 
                    Console.WriteLine("Incorrect Username!");
                }
            }
            
            
        }

  


    }

    public enum Classification { G, PG, M15, MA15 };
    public enum Gener { Drama, Adventure, Family, Action, SciFi, Comedy, Thriller, Other };

    public class Movie {
        private string movietitle;
        private string starring;
        private string director;
        private Classification _classification;
        private int duration;
        private Gener _gener;
        private int releasedate;
        private int copies;
        private int renttimes;

        public Movie(string movietitle, string starring, string director, Classification classification, int duration, Gener gener, int releasedate, int copies) { 
            this.movietitle = movietitle;
            this.starring = starring;
            _gener = gener;
            this.director = director;
            _classification = classification;
            this.duration = duration;
            this.releasedate = releasedate;
            this.copies = copies;
            this.renttimes = 0;
        }

        public string Movietitle { get => movietitle; set => movietitle = value; }
        public string Starring { get => starring; set => starring = value; }
        public string Director { get => director; set => director = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Releasedate { get => releasedate; set => releasedate = value; }
        public Classification Classfication { get => _classification; set => _classification = value; }
        public Gener Gener { get => _gener; set => _gener = value; }
        public int Copies { get => copies; set => copies = value; }
        public int Renttimes { get => renttimes; set => renttimes = value; }

        // Display Movie information
        public override string ToString() {
            Console.WriteLine("Title: {0}", Movietitle);
            Console.WriteLine("Starring: {0}", Starring);
            Console.WriteLine("Director: {0}", Director);
            Console.WriteLine("Gener: {0}", Gener);
            Console.WriteLine("Classification: {0}", Classfication);
            Console.WriteLine("Duration: {0}", Duration);
            Console.WriteLine("Release Date: {0}", Releasedate);
            Console.WriteLine("Copies available: {0}", Copies);
            Console.WriteLine("Times Rented: {0}\n\n", Renttimes);
            return base.ToString();
        }
    }

    public class Node {
        
        private Movie movie;
        
        private Node left;
        
        private Node right;
        
        public Node(Movie movie) { 
            this.movie = movie;
            this.left = null;
            this.right = null;
        }

        public Movie Movie { get => movie; set => movie = value; }
        public Node Left { get => left; set => left = value; }
        public Node Right { get => right; set => right = value; }
    }

    public class BinarySearchTree {        
        //root
        private Node rootnode;


        private static int count = 0;
        // creat a array to store movie in BSTree node
        private static Movie[] displaymovie = new Movie[count];

        public BinarySearchTree() {
            this.rootnode = null;
        }

        // Add movie as a node in BSTree with suitable situation
        public void Insert(Movie movie) {
            Node newnode = new Node(movie); 
            Node parent;
            if (rootnode == null) { // root = null
                rootnode = newnode;
            } else {
                Node current = rootnode;
                while (true) {
                    parent = current;
                    if (newnode.Movie.Movietitle.CompareTo(current.Movie.Movietitle) < 0) {
                        current = current.Left;
                        if (current == null) {
                            parent.Left = newnode;                        
                            break;
                        }
                    } else {
                        current = current.Right;
                        if (current == null) {
                            parent.Right = newnode;                        
                            break;
                        }
                    }
                }
            }
        }

        // Delete movie in BSTree
        // 1.current node has no children
        // 2.current node has one child
        // 3.current node has two children
        public void Delete(string movietitle) { 
            Node current = rootnode; // current node
            Node parent = rootnode;
            if(current != null) { 
                while(current.Movie.Movietitle != movietitle) { 
                    parent = current;
                    if(current.Movie.Movietitle.CompareTo(movietitle) > 0) { 
                        current = current.Left;
                    } else { 
                        current = current.Right;
                    }
                }

                if (current.Left == null && current.Right == null) { // circumstance 1
                    if (current == rootnode) { 
                        rootnode = null;
                    }
                    if (current == parent.Left) { 
                        parent.Left = null;
                    }else { 
                        parent.Right = null;
                    }                
                } else if (current.Left!= null && current.Right == null) { // circumstance 2: right child
                    if (current == rootnode) { 
                        rootnode = current.Left;
                    }
                    if(current == parent.Left) { 
                        parent.Left = current.Left;
                    } else { 
                        parent.Right = current.Left;
                    }
                } else if (current.Left == null && current.Right != null) { // circumstance 2: left child
                    if (current == rootnode) { 
                        rootnode = current.Right;
                    }
                    if (current == parent.Left) {
                        parent.Left = current.Right;
                    } else {
                        parent.Right = current.Right;
                    }
                } else if(current.Left != null && current.Right != null) { // circumstance 3
                    if (current.Left.Right == null) { 
                        current.Movie = current.Left.Movie;
                        current.Left = current.Left.Left;
                    } else { 
                        Node tempcurrentleft = current.Left;
                        Node tempcurrent = current;
                        while(tempcurrentleft.Right != null) { 
                            tempcurrent = tempcurrentleft;
                            tempcurrentleft = tempcurrentleft.Right;
                        }
                        current.Movie = tempcurrentleft.Movie;
                        tempcurrent.Right = tempcurrentleft.Left;
                    }
                }
            }
            
        }

        // according to input movietitle check if movie in BSTree
        // if it exists, return true; not exist return false
        public bool find(string movietitle) {
            Node current = rootnode;
            while (current != null) {
                if (current.Movie.Movietitle.CompareTo(movietitle) == 0) {
                    return true;
                } else if (current.Movie.Movietitle.CompareTo(movietitle) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            return false;
        }

        //according to input movietitle return movie in BSTree
        public Movie GetMovie(string movietitle) {
            Node current = rootnode;
            while (current != null && current.Movie.Movietitle != movietitle) {
                if (current.Movie.Movietitle.CompareTo(movietitle) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            return current.Movie;
        }

        //Inorder traverse to display movie information contructor
        public void InOrder() {           
            InOrderDisplay(rootnode);            
        }

        // Inorder traverse to display movie information
        private void InOrderDisplay(Node rootnode) {
            if (rootnode != null) {
                InOrderDisplay(rootnode.Left);
                rootnode.Movie.ToString();
                InOrderDisplay(rootnode.Right);
            }
        }


        // Inorder traverse and transform BSTree to Array contructor
        public Movie[] InOrderTraverse() {
            InOrderArray(rootnode);
            Movie[] temp = displaymovie;

            
            count = 0; // initial count
            displaymovie = new Movie[count]; // initial array
            
            return temp;

        }
        
        // Inorder traverse and transform BSTree to Array
        private Movie[] InOrderArray(Node rootnode) {
            
            if (rootnode != null) {
                InOrderArray(rootnode.Left);               
                count += 1;
                Movie[] temp = displaymovie;
                displaymovie = new Movie[count];
                temp.CopyTo(displaymovie, 0);
                displaymovie[count - 1] = rootnode.Movie;
                InOrderArray(rootnode.Right);
            }             
            return displaymovie;
        }

    }
    public class MovieCollection {
        public static BinarySearchTree moviecollection = new BinarySearchTree();      
        
        // Display addmovie menu and addmovie in BSTree
        public static void AddMovie() {
            string movietitle;
            string starring;
            string director;
            Classification classification = new Classification();
            int duration;
            Gener gener = new Gener();
            int releasedate;
            int copies;
            bool inputclassfication;
            bool inputgener;
             

            Console.Write("Enter the movie title: ");
            movietitle = Console.ReadLine();
            if (moviecollection.find(movietitle) == true) { // if movie exists, add copies               
                Console.Write("Enter the number of copies you would like to add: ");
                moviecollection.GetMovie(movietitle).Copies += int.Parse(Console.ReadLine());             
            } else { // create new movie
                Console.Write("Enter the starring actor(s): ");
                starring = Console.ReadLine();
                Console.Write("Enter the director(s): ");
                director = Console.ReadLine();
                Console.Write("\nSelect the genre: \n1. Drama\n2. Adventure\n3. Family\n4. Action\n5. Sci-Fi\n6. Comedy\n7. Thrille\n8. Other\nMake Selection(1-8): ");
            
                do {
                    inputgener = true;
                    switch (Console.ReadLine()) {
                    case "1":
                        gener = Gener.Drama;
                        break;
                    case "2":
                        gener = Gener.Adventure;
                        break;
                    case "3":
                        gener = Gener.Family;
                        break;
                    case "4":
                        gener = Gener.Action;
                        break;
                    case "5":
                        gener = Gener.SciFi;
                        break;
                    case "6":
                        gener = Gener.Comedy;
                        break;
                    case "7":
                        gener = Gener.Thriller;
                        break;
                    case "8":
                        gener = Gener.Other;
                        break;
                    default:
                        Console.Write("Invalid input! Please try again: ");
                        inputgener = false;
                        break;                  
                    }
                } while (inputgener == false);
                Console.Write("\nSelect the classification: \n1. General (G)\n2. Parental Guidance (PG)\n3. Mature (M15+)\n4. Mature Accompanied (MA15+)\nMake Selection(1-4): ");
                do { 
                    inputclassfication = true;
                    switch (Console.ReadLine()) { 
                        case "1":
                            classification = Classification.G;
                            break;
                        case "2":
                            classification = Classification.PG;
                            break;
                        case "3":
                            classification = Classification.M15;
                            break;
                        case "4":
                            classification = Classification.MA15;
                            break;
                        default:
                            Console.Write("Invalid Input! Please try again: ");
                            inputclassfication = false;
                            break;
                    }
                
                } while(inputclassfication == false);
            

                Console.Write("Enter the duration (minutes): ");
                duration = int.Parse(Console.ReadLine());
                Console.Write("Enter the release date (year): ");
                releasedate = int.Parse(Console.ReadLine());
                Console.Write("Enter the number of copies available: ");
                copies = int.Parse(Console.ReadLine());
            

                Movie movie = new Movie(movietitle, starring, director, classification, duration, gener, releasedate, copies);
           
                moviecollection.Insert(movie);
            }


        }

        // Display remove movie menu and delete movie in BSTree
        public static void RemoveMovie() {            
            Console.Write("Enter the movie: "); 
            string movietitle = Console.ReadLine();
            if (moviecollection.find(movietitle) == true) { // if movie exists, remove movie
                Console.WriteLine("{0} was deleted", movietitle);
                moviecollection.Delete(movietitle);
            } else { 
                Console.WriteLine("{0} is not exist!", movietitle);
            }

            
        }

        // Display all movie information
        public static void DisplayAllMovieInfo() { 
            moviecollection.InOrder();
           
        }    

        // Display top10 movie with renttimes descending order
        public static void DisplayTopMovie() {
            Movie[] display = moviecollection.InOrderTraverse();
            Movie temp;
            for(int i =0; i < display.Length - 1; i++) { 
                for(int j = 0; j < display.Length - 1 - i; j++) { 
                    if(display[j].Renttimes < display[j + 1].Renttimes) { 
                        temp = display[j];
                        display[j] = display[j + 1];
                        display[j + 1] = temp;
                    }
                }
            }

            foreach (Movie movie in display) { 
                Console.WriteLine("{0} borrowed {1} times.", movie.Movietitle, movie.Renttimes);
            }
            
        }
          
    }


    public class Member { 
        private string firstname;
        private string lastname;
        private string username;
        private string address;
        private int phone;
        private int password; 
        private List<Movie> borrowedmovie;
        public Member(string firstname, string lastname, string username,string address, int phone, int password, List<Movie> borrowedmovie) { 
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.address = address;
            this.phone = phone;
            this.password = password;
            this.borrowedmovie = borrowedmovie;;
        }

        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Username { get => username; set => username = value; }
        public string Address { get => address; set => address = value; }
        public int Phone { get => phone; set => phone = value; }
        public int Password { get => password; set => password = value; }
        public List<Movie> Borrowedmovie { get => borrowedmovie; set => borrowedmovie = value; }

    }


    public class MemberCollection {        
        private static int membermount = 0;
        private static Member member;
        private static Member[] membercollection = new Member[membermount];
        
        // Display member menu
        public static void MemberMenu() {
            while (true) { 
                Console.Write("Enter your Username(LastnameFirstname): ");
                string username = Console.ReadLine(); 
                for (int i = 0; i < membercollection.Length; i++) { //verify member's username with input username
                    if(membercollection[i].Username == username) { 
                        Console.Write("Enter password: ");
                        int password = int.Parse(Console.ReadLine());
                        if (password == membercollection[i].Password) { // verify password 
                            member = membercollection[i];
                            while(true){ 
                                Console.WriteLine("\n\n===========Member Menu===========");
                                Console.WriteLine("1. Display all movies");
                                Console.WriteLine("2. Borrow a movie DVD");
                                Console.WriteLine("3. Return a movie DVD");
                                Console.WriteLine("4. List current borrowed movie DVDs");
                                Console.WriteLine("5. Display top 10most popular movies");
                                Console.WriteLine("0. Return to main menu");
                                Console.Write("\nPlease make a selection (1-5, or 0 to exit to main menu): ");
                                switch (Console.ReadLine()) {
                                    case "1":
                                        MovieCollection.DisplayAllMovieInfo();
                                        break;
                                    case "2":
                                        MemberCollection.BorrowMovie();
                                        break;
                                    case "3":
                                        MemberCollection.ReturnMovie();
                                        break;
                                    case "4":
                                        MemberCollection.ListMovie();
                                        break;
                                    case "5":
                                        MovieCollection.DisplayTopMovie();
                                        break;
                                    case "0":
                                        Program.MainMenu();
                                        return;
                                    default:
                                        Console.Write("Invalid input! Please try again: ");
                                        break;
                                }
                            }
                           
                        } else { 
                            Console.WriteLine("Password is not correct!");
                            }

                    } 
                }
                Console.WriteLine("{0} is not exist!", username);                ;
            }           
        }

        // Register member and store member in array membercollection
        public static void Registermember() {
            string firstname;
            string lastname;
            string username;
            string address;
            int phone;
            int password;
            List<Movie> borrowedmovie = new List<Movie>();
            Console.Write("\nEnter member's first name: ");
            firstname = Console.ReadLine();
            Console.Write("Enter member's last name: ");
            lastname = Console.ReadLine();
            Console.Write("Enter member's address: ");
            username = lastname + firstname;
            for (int i = 0; i < membercollection.Length; i++) { // check if member has been registered
                if (membercollection[i].Username == username) { 
                    Console.WriteLine("{0} {1} has already registered.", firstname, lastname);
                    return;
                }                    
            }
            address = Console.ReadLine();
            Console.Write("Enter member's phone number: ");
            phone = int.Parse(Console.ReadLine());
            Console.Write("Enter member's password(4 digits): ");
            password = int.Parse(Console.ReadLine());
            

            Member member = new Member(firstname, lastname, username, address, phone, password, borrowedmovie);
            
            membermount += 1;
            Member[] temp = membercollection;
            membercollection = new Member[membermount];
            temp.CopyTo(membercollection, 0);
            membercollection[membermount - 1] = member;
           
        }

        // Member borrowe movie according to input movie title
        public static void BorrowMovie() {
            
            Console.Write("Enter movie title: ");
            string borrowdvd = Console.ReadLine();
            if (MovieCollection.moviecollection.GetMovie(borrowdvd).Copies > 0) { // check movie copies available
                Console.WriteLine("You borrowed {0}.", borrowdvd);
                member.Borrowedmovie.Add(MovieCollection.moviecollection.GetMovie(borrowdvd));
                MovieCollection.moviecollection.GetMovie(borrowdvd).Copies -= 1;
                MovieCollection.moviecollection.GetMovie(borrowdvd).Renttimes += 1;
            } else {
                Console.WriteLine("{0} has no cpopies", borrowdvd);
            }  
            
        }

        // Member return movie according to input movie title
        public static void ReturnMovie() {
            Console.Write("Enter movie title: ");
            string returndvd = Console.ReadLine();
            Console.WriteLine("You ruturned {0}.", returndvd);
            if (member.Borrowedmovie.Contains(MovieCollection.moviecollection.GetMovie(returndvd))) { // check member has that movie
                member.Borrowedmovie.Remove(MovieCollection.moviecollection.GetMovie(returndvd));
                MovieCollection.moviecollection.GetMovie(returndvd).Copies += 1;
            } else { 
                Console.WriteLine("You did not borrowed it, you cannot return!");
            }

        }

        // List member currently borrowing movie
        public static void ListMovie() { 
            Console.WriteLine("You are currently borrowing: ");
            foreach(Movie movie in member.Borrowedmovie) { 
                Console.WriteLine(movie.Movietitle);
            }
        }

        // Display members name and their phone number
        public static void FindPhoneNum() { 
            
            for (int i = 0; i < membercollection.Length; i++) { 
                Console.WriteLine("Name:{0} {1} \t\t Phone Number:{2}", membercollection[i].Firstname, membercollection[i].Lastname, membercollection[i].Phone);
            }
        }
    }
     
}


