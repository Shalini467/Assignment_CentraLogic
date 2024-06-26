﻿using System.Net;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LibraryManagementSystem.Controllers
{



    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class MembersController
    {

        //Here we 4 entity to connect our CosmosDatabase

        private string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagementSystem";
        public string ContainerName = "LibraryManagementDB";

        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        public MembersController()
        {
            container = GetContainer();
        }

        //Add new member
        [HttpPost]
        public async Task<Member> AddMember(Member member)
        {
           
            {
                MemberEntity entity = new MemberEntity();
                entity.Name = member.Name;
                entity.DateOfBirth = member.DateOfBirth;
                entity.Email= member.Email;
                

                entity.Id = Guid.NewGuid().ToString();
                entity.UId = entity.Id;
                entity.DocumentType = "Member";
                entity.CreatedBy = "Shalini";
                entity.CreatedOn = DateTime.Now;
                entity.UpdatedBy = "Shalini";
                entity.UpdatedOn = DateTime.Now;
                entity.Version = 1;
                entity.Active = true;
                entity.Archived = false;

                MemberEntity response = await container.CreateItemAsync(entity);

                Member responseMember= new Member();
                responseMember.Name = response.Name;
                responseMember.DateOfBirth = response.DateOfBirth;
                responseMember.Email = response.Email;
                
                return responseMember;
            }

        }


        //Retrieve member by UId
        [HttpGet]
        public async Task<Member> GetMemberByUid(string uId)
        {
            var getMember = container.GetItemLinqQueryable<Member>(true).Where(q => q.UId == uId).FirstOrDefault();
            return getMember;
        }


       
        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        private IActionResult Ok(Member resource)
        {
            throw new NotImplementedException();
        }


        //Get all members
        [HttpGet]
        public async Task<List<Member>> GetAllMembers()
        {
            var allMembers = container.GetItemLinqQueryable<Member>(true).ToList();
            return allMembers;

        }



        //Update member
        [HttpPost]
        public async Task<Member> UpdateMember(Member member)
        {
            var existingMemberResponse = await container.ReadItemAsync<Member>(member.UId, new PartitionKey(member.UId));
            if (existingMemberResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException("Member not found.");
            }

            var existingMember = existingMemberResponse.Resource;

            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;

            var updatedMemberResponse = await container.ReplaceItemAsync(existingMember, existingMember.UId, new PartitionKey(existingMember.UId));

            if (updatedMemberResponse.StatusCode == HttpStatusCode.OK)
            {
                return updatedMemberResponse.Resource;
            }
            else
            {
                throw new Exception("Failed to update the member.");
            }
        }
    }

}

