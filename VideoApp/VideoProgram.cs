using System;
using CustomerAppUI.Model;
using VideoAppBLL;
using VidepAppEntity;

namespace VideoAppGUI
{
    internal static class VideoProgram
    {
        private static readonly BLLFacade BLLFacade = new BLLFacade();
        private static readonly MenuModel MenuModel = new MenuModel();
        private static bool _userIsDone;

        private static void Main(string[] args)
        {
            while (!_userIsDone)
            {
                ShowMenu(MenuModel.MenuItems);

                var userSelection = GetInputFromUser();

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
        /// Prompt user for id to edit a Video
        /// </summary>
        private static void EditVideo()
        {
            var video = FindVideoById();
            if (video != null)
            {
                Console.Write("Title: ");
                video.Title = Console.ReadLine();
                var updatedVideo = BLLFacade.VideoService.Update(video);
                Console.WriteLine("\nVideo updated!");
                DisplayVideo(updatedVideo);
            }
            else
            {
                Console.WriteLine("Video not Found!");
            }
        }

        /// <summary>
        ///     Prompt user for id to find Video
        /// </summary>
        /// <returns>Video with parsed id</returns>
        private static Video FindVideoById()
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

            var createdVideo = BLLFacade.VideoService.Create(new Video
            {
                Title = title
            });
            Console.WriteLine("\nVideo created:");
            DisplayVideo(createdVideo);
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

        private static void DisplayVideo(Video videoToDisplay)
        {
            Console.WriteLine($"Id: {videoToDisplay.Id} Title: {videoToDisplay.Title}");
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
        private static int GetInputFromUser()
        {
            Console.Write("Your input: ");
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection < 1
                   || selection > 5)
                Console.WriteLine("Please select a number between 1-5");

            return selection;
        }
    }
}