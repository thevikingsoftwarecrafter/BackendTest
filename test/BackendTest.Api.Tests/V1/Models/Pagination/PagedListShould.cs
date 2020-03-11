using System.Collections.Generic;
using AutoFixture.Xunit2;
using BackendTest.Api.V1.Models.Pagination;
using FluentAssertions;
using Xunit;

namespace BackendTest.Api.Tests.V1.Models.Pagination
{
    public class PagedListShould
    {
        [Theory, AutoData]
        public void Compute_Zero_Pages_When_Source_List_Is_Empty(int pageNumber, int pageSize)
        {
            //Arrange
            var sourceList = new List<string>();

            //Act
            var pagedList = PagedList<string>.ToPagedList(sourceList, pageNumber, pageSize);

            //Assert
            pagedList.CurrentPage.Should().Be(pageNumber);
            pagedList.PageSize.Should().Be(pageSize);
            pagedList.TotalCount.Should().Be(0);
            pagedList.TotalPages.Should().Be(0);
        }

        [Theory, AutoData]
        public void Compute_Pages_When_Source_List_Is_Not_Empty(int pageNumber, int pageSize, List<string> sourceList)
        {
            //Arrange
            //Act
            var pagedList = PagedList<string>.ToPagedList(sourceList, pageNumber, pageSize);

            //Assert
            pagedList.CurrentPage.Should().Be(pageNumber);
            pagedList.PageSize.Should().Be(pageSize);
            pagedList.TotalCount.Should().Be(sourceList.Count);
        }
    }
}