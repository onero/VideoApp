using System;
using CustomerAppUI.Model;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

namespace VideoAppGUI
{
    internal static class VideoProgram
    {
        private const string Yes = "y";
        private static readonly BLLFacade BLLFacade = new BLLFacade();
        private static readonly MenuModel MenuModel = new MenuModel();
        private static bool _userIsDone;

        private static void Main(string[] args)
        {
            while (!_userIsDone)
            {
                ShowMenu(MenuModel.MenuItems);

                var userSelection = GetInputFromUser(MenuModel.MenuItems.Length);

                ReactToUserInput(userSelection);
            }

            Console.WriteLine("Bye bye!");
        }

        /// <summary>
        ///     React to the user input
        /// </summary>
        /// <param name="selection"></param>
        private static void ReactToUserInput(int selection)
        {
            switch (selection)
            {
                case 1:
                    ListVideos();
                    break;
                case 2:
                    AddVideos();
                    break;
                case 3:
                    ListVideos();
                    DeleteVideo();
                    break;
                case 4:
                    ListVideos();
                    EditVideo();
                    break;
                case 5:
                    _userIsDone = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Prompt user for id to edit a Video
        /// </summary>
        private static void EditVideo()
        {
            var video = FindVideoById();
            if (video != null)
            {
                // Prompt Title
                var userWantsToEditTitle = PromptUserForEdit("title");
                if (userWantsToEditTitle)
                {
                    Console.Write("Title: ");
                    video.Title = Console.ReadLine();
                }

                // Prompt for Genre
                var userWantsToEditGenre = PromptUserForEdit("genre");
                if (userWantsToEditGenre)
                    video.Genre = GetGenreFromUser();

                var updatedVideo = BLLFacade.VideoService.Update(video);
                Console.WriteLine("\nVideo updated!");
                DisplayVideo(updatedVideo);
            }
            else
            {
                Console.WriteLine("Video not Found!");
            }
        }

        private static bool PromptUserForEdit(string choiceToEdit)
        {
            var validUserInput = false;
            bool userResponse;
            do
            {
                Console.Write($"Would you like to change the {choiceToEdit}? ('y' = yes, 'n' = no): ");
                var userResponseAsString = Console.ReadLine().Normalize().ToLower();

                if (userResponseAsString.Equals(Yes) || userResponseAsString.Equals("n"))
                    validUserInput = true;
                userResponse = userResponseAsString.Equals(Yes);
            } while (!validUserInput);
            return userResponse;
        }

        /// <summary>
        ///     Prompt user for id to find Video
        /// </summary>
        /// <returns>Video with parsed id</returns>
        private static VideoBO FindVideoById()
        {
            Console.Write("Insert Video Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Please insert a number");
            return BLLFacade.VideoService.GetById(id);
        }

        /// <summary>
        ///     Prompt user for id for Video to delete
        /// </summary>
        private static void DeleteVideo()
        {
            Console.WriteLine("Please write id of Video to delete");
            var videoFound = FindVideoById();
            if (videoFound != null)
                BLLFacade.VideoService.Delete(videoFound.Id);

            var response = videoFound == null ? "Video not found" : "Video was deleted";
            Console.WriteLine($"\n{response}");
        }

        /// <summary>
        ///     Prompt user for information to add new Video
        /// </summary>
        private static void AddVideos()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            var genre = GetGenreFromUser();
            var createdVideo = BLLFacade.VideoService.Create(new VideoBO
            {
                Title = title,
                Genre = genre
            });
            Console.WriteLine("\nVideo created:");
            DisplayVideo(createdVideo);
        }

        private static GenreBO GetGenreFromUser()
        {
            Console.WriteLine();
            GenreBO chosenGenre;
            var genres = Enum.GetNames(typeof(GenreBO));
            foreach (var genre in genres)
                Console.WriteLine($"Genre: {genre}");
            bool genreAccepted;
            do
            {
                Console.Write($"Please write genre name: ");
                var genre = Console.ReadLine();
                genreAccepted = Enum.TryParse(genre, true, out chosenGenre);
                if (!genreAccepted)
                {
                    Console.WriteLine($"{genre} is not on our list of genres...");
                    Console.WriteLine("We currently don't support adding genres, but this may come in the future");
                }
            } while (!genreAccepted);
            return chosenGenre;
        }

        /// <summary>
        ///     List all Videos
        /// </summary>
        private static void ListVideos()
        {
            Console.WriteLine("\nList of Videos");
            foreach (var video in BLLFacade.VideoService.GetAll())
                DisplayVideo(video);
        }

        private static void DisplayVideo(VideoBO videoToDisplay)
        {
            Console.WriteLine($"Id: {videoToDisplay.Id} Title: {videoToDisplay.Title} Genre: {videoToDisplay.Genre}");
        }


        private static void ShowMenu(string[] menuItems)
        {
            Console.WriteLine("\nSelect What you want to do:\n");

            for (var i = 0; i < menuItems.Length; i++)
                //Console.WriteLine((i + 1) + ":" + menuItems[i]);
                Console.WriteLine($"{i + 1}: {menuItems[i]}");
        }

        /// <summary>
        ///     Get input from user
        /// </summary>
        /// <returns></returns>
        private static int GetInputFromUser(int max)
        {
            Console.Write("Your input: ");
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection < 1
                   || selection > max)
                Console.WriteLine($"Please select a number between 1-{max}");

            return selection;
        }
    }
}