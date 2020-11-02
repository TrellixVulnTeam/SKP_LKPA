﻿using Dancord.Classes.Base;
using Dancord.Classes.Channels;
using Dancord.Classes.Members;
using Dancord.Classes.Roles;
using Dancord.Classes.Users;
using System;
using System.Windows;

namespace Dancord.Classes
{
    class Server
    {
        #region Own Events
        public delegate void OnMemberJoin(ServerMember member);
        public delegate void OnMemberLeave(ServerMember member);
        #endregion

        #region Events
        public OnMemberJoin OnMemberJoining;
        public OnMemberLeave OnMemberLeaving;
        public OnDelete<Server> OnDelete;
        #endregion

        #region General Properties
        public Name Name { get; }
        public DateTime CreatedAt { get; }
        #endregion

        #region Managers
        public ChannelManager Channels { get; }
        public ServerMemberManager ServerMembers { get; }
        public RoleManager Roles { get; }
        #endregion



        public Server(string name)
        {
            this.Name = new Name(name);
            this.CreatedAt = DateTime.Now;

            Roles = new RoleManager(true);
            
            OnMemberLeaving += OnLeft;
            OnMemberJoining += ServerMembers.OnJoined;

        }

        public void AddMember(User user)
        {
            ServerMember member = new ServerMember(user);
            member.OnLeaving += ServerMembers.OnLeft;
            OnMemberJoining(member);
        }
        public void OnLeft(ServerMember member)
        {
            if (member.IsOwner && MessageBox.Show($"Are you sure you want to DELETE \"{Name}\"?", $"Delete {Name}?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    OnDelete(this);
        }
    }
}
