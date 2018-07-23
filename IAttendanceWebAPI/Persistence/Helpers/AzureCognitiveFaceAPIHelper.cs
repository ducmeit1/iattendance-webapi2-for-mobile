using IAttendanceWebAPI.Core.ViewModels;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Helpers
{
    public class AzureCognitiveFaceApiHelper
    {
        private static readonly string ApiCognitiveServiceKey =
            ConfigurationManager.AppSettings["SubscriptionKeyMicrosoftCognitive"];

        private static readonly string ServerName = "southeastasia";

        public static string PersonGroupId = "iattendance";

        private static readonly string EndPoint = $"https://{ServerName}.api.cognitive.microsoft.com/face/v1.0";

        private static readonly IFaceServiceClient FaceServiceClient =
            new FaceServiceClient(ApiCognitiveServiceKey, EndPoint);


        public static async Task CreatePersonGroup(PersonGroupBindingModel model)
        {
            await FaceServiceClient.CreatePersonGroupAsync(model.Id.ToLower(), model.Name);
        }


        public static async Task<string> CreatePersonInPersonGroup(PersonBindingModel model)
        {
            var createPersonResult =
                await FaceServiceClient.CreatePersonInPersonGroupAsync(model.PersonGroupId, model.Name);

            foreach (var faceUrl in model.FacesUrl)
                await FaceServiceClient.AddPersonFaceInPersonGroupAsync(model.PersonGroupId,
                    createPersonResult.PersonId, faceUrl);

            return createPersonResult.PersonId.ToString();
        }

        public static async Task AddFacesInPerson(AddFacesBindingModel model)
        {
            foreach (var faceUrl in model.FacesUrl.ToArray())
                await FaceServiceClient.AddPersonFaceInPersonGroupAsync(model.PersonGroupId,
                    model.PersonId, faceUrl);
        }

        public static async Task TrainingThePersonGroup(string personGroupId)
        {
            await FaceServiceClient.TrainPersonGroupAsync(personGroupId);
            while (true)
            {
                var trainingStatus = await FaceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);
                if (trainingStatus.Status != Status.Running) break;
                await Task.Delay(1000);
            }
        }

        public static async Task<IEnumerable<IdentifiedPersonModel>> RecognizeFaces(RecognizeFacesBindingModel model)
        {
            var identifiedPersons = new List<IdentifiedPersonModel>();
            var faceIds = new List<Guid>();
            foreach (var faceUrl in model.FacesUrl)
            {
                var faces = await FaceServiceClient.DetectAsync(faceUrl);
                faceIds.AddRange(faces.Select(f => f.FaceId));
            }

            if (faceIds.Count > 0)
            {
                var faceIdsArray = faceIds.ToArray();
                var identifyResults =
                    await FaceServiceClient.IdentifyAsync(faceIdsArray, model.PersonGroupId);
                foreach (var identifyResult in identifyResults)
                    if (identifyResult.Candidates.Length != 0)
                    {
                        foreach (var candidate in identifyResult.Candidates)
                        {
                            if (candidate.Confidence >= 0.5)
                            {
                                var person =
                                    await FaceServiceClient.GetPersonInPersonGroupAsync(model.PersonGroupId,
                                        candidate.PersonId);
                                identifiedPersons.Add(new IdentifiedPersonModel
                                {
                                    Name = person.Name,
                                    PersonId = person.PersonId.ToString()
                                });
                            }
                        }
                    }
            }

            return identifiedPersons.Distinct();
        }
    }
}